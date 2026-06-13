import React, { useState } from 'react';
import toraService from "../../services/toraService";
import './Sgula.css';

const Sgula = () => {
    const [name, setName] = useState('');
    const [results, setResults] = useState([]);
    const [loading, setLoading] = useState(false);
    const [searchDone, setSearchDone] = useState(false);

    const findSgula = async (e) => {
        e.preventDefault();
        if (!name.trim()) return;
        
        setLoading(true);
        setSearchDone(false);
        try {
            const data = await toraService.getPsukimForSgula(name);
            setResults(data || []);

            // שמירה להיסטוריה
            await toraService.createLineToHistory("חיפוש פסוק לסגולה", name, "הכל", "הכל", "הכל");

        } catch (err) {
            console.error("Error fetching sgula:", err);
        } finally {
            setLoading(false);
            setSearchDone(true);
        }
    };

    const renderHighlightedPasuk = (text) => {
        if (!text) return '';
        const hebrewRegex = /[\u05D0-\u05EA]/;
        let first = -1, last = -1;
        for (let i = 0; i < text.length; i++) {
            if (hebrewRegex.test(text[i])) { first = i; break; }
        }
        for (let i = text.length - 1; i >= 0; i--) {
            if (hebrewRegex.test(text[i])) { last = i; break; }
        }
        if (first === -1) return text;
        return (
            <>
                {text.substring(0, first)}
                <span className="highlight-letter">{text[first]}</span>
                {text.substring(first + 1, last)}
                {first !== last && <span className="highlight-letter">{text[last]}</span>}
                {text.substring(last + 1)}
            </>
        );
    };

    return (
        <div className="sgula-page-container">
            <div className="sgula-header">
                <h1>הפסוק שלך לסגולה</h1>
                <p className="subtitle">
                    סגולה לומר בכל יום בסוף התפילה פסוק המתחיל באות הראשונה של שמך ומסתיים באות האחרונה.
                </p>
            </div>
            <div className="sgula-search-area">
                <form onSubmit={findSgula} className="sgula-form">
                    <input 
                        type="text" 
                        value={name} 
                        onChange={(e) => setName(e.target.value)} 
                        placeholder="הכנס את שמך הפרטי..."
                        className="sgula-input"
                    />
                    <button type="submit" className="sgula-submit-btn" disabled={loading}>
                        {loading ? 'מחפש...' : 'מצא את הפסוק שלי'}
                    </button>
                </form>
            </div>
            <div className="sgula-results-container">
                {loading && <div className="loader">מחפש פנינים בתורה...</div>}
                {searchDone && results.length > 0 && (
                    <div className="sgula-results-grid">
                        {results.map((r, i) => (
                            <div key={i} className="sgula-result-card">
                                <div className="card-decor top"></div>
                                <p className="pasuk-text">{renderHighlightedPasuk(r.text)}</p>
                                <div className="pasuk-source">
                                    <span>חומש {r.chumashName}</span>
                                    <span className="separator">|</span>
                                    <span>{r.parashaName}</span>
                                    <span className="separator">|</span>
                                    <span>פרק {r.perekName}</span>
                                </div>
                                <div className="card-decor bottom"></div>
                            </div>
                        ))}
                    </div>
                )}
                {searchDone && results.length === 0 && (
                    <div className="sgula-no-results">
                        <p>לא נמצאו פסוקים מתאימים לשם "{name}".</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Sgula;