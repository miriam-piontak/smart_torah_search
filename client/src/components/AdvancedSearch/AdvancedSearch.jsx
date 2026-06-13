import React, { useState } from 'react';
import toraService from "../../services/toraService";
import './AdvancedSearch.css';

const AdvancedSearch = () => {
    const [word, setWord] = useState('');
    const [searchType, setSearchType] = useState('sofei');
    const [results, setResults] = useState([]);
    const [loading, setLoading] = useState(false);

    const handleSearch = async (e) => {
        e.preventDefault();
        if (!word.trim()) return;
        setLoading(true);
        setResults([]);
        try {
            let data;
            let typeLabel = "";
            if (searchType === 'sofei') {
                data = await toraService.getSofeiTevot(word);
                typeLabel = "סופי תיבות";
            } else if (searchType === 'rashei') {
                data = await toraService.getRasheiTevot(word);
                typeLabel = "ראשי תיבות";
            } else if (searchType === 'gimatria') {
                data = await toraService.getGimatria(word);
                typeLabel = "גימטריה";
            }
            
            setResults(data || []);
            
            // שמירה להיסטוריה
            await toraService.createLineToHistory(typeLabel, word, "הכל", "הכל", "הכל");

        } catch (err) {
            console.error(err);
            alert("שגיאה בחיבור לשרת");
        } finally {
            setLoading(false);
        }
    };

    const getGimatriaValue = (str) => {
        const map = {'א':1,'ב':2,'ג':3,'ד':4,'ה':5,'ו':6,'ז':7,'ח':8,'ט':9,'י':10,'כ':20,'ל':30,'מ':40,'נ':50,'ס':60,'ע':70,'פ':80,'צ':90,'ק':100,'ר':200,'ש':300,'ת':400,'ך':20,'ם':40,'ן':50,'ף':80,'ץ':90};
        const clean = str.replace(/[^\u05D0-\u05EA]/g, "");
        return clean.split('').reduce((sum, char) => sum + (map[char] || 0), 0);
    };

    const renderHighlightedText = (text) => {
        const words = text.split(/\s+/);
        const target = word.trim();
        let matchStartIndex = -1;

        if (searchType !== 'gimatria' && target.length > 0) {
            for (let i = 0; i <= words.length - target.length; i++) {
                const currentSequence = words.slice(i, i + target.length).map(w => {
                    const onlyLetters = w.replace(/[^\u05D0-\u05EA]/g, "");
                    return searchType === 'sofei' ? onlyLetters.slice(-1) : onlyLetters.slice(0, 1);
                }).join('');
                if (currentSequence === target) {
                    matchStartIndex = i;
                    break;
                }
            }
        }

        return words.map((w, i) => {
            const onlyLetters = w.replace(/[^\u05D0-\u05EA]/g, "");
            if (searchType === 'gimatria') {
                const isMatch = getGimatriaValue(onlyLetters) === parseInt(target);
                return <React.Fragment key={i}><span className={isMatch ? "highlight-gimatria" : ""}>{w}</span> </React.Fragment>;
            }
            const isPartOfSequence = i >= matchStartIndex && i < matchStartIndex + target.length;
            if (isPartOfSequence) {
                const chars = w.split('');
                let targetIdx = -1;
                if (searchType === 'rashei') {
                    targetIdx = chars.findIndex(c => /[\u05D0-\u05EA]/.test(c));
                } else {
                    for (let j = chars.length - 1; j >= 0; j--) {
                        if (/[\u05D0-\u05EA]/.test(chars[j])) {
                            targetIdx = j;
                            break;
                        }
                    }
                }
                return (
                    <React.Fragment key={i}>
                        <span>
                            {chars.map((char, charIdx) => (
                                <span key={charIdx} className={charIdx === targetIdx ? "color-letter" : ""}>{char}</span>
                            ))}
                        </span>{' '}
                    </React.Fragment>
                );
            }
            return <span key={i}>{w} </span>;
        });
    };

    return (
        <div className="mainDiv">
            <h1 className="main-title">חיפושים מתקדמים</h1>
            <form onSubmit={handleSearch} className="searchForm">
                <select className="search-select" value={searchType} onChange={(e) => setSearchType(e.target.value)}>
                    <option value="sofei">סופי תיבות</option>
                    <option value="rashei">ראשי תיבות</option>
                    <option value="gimatria">גימטריה</option>
                </select>
                <input 
                    className="search-input" 
                    value={word} 
                    onChange={(e) => setWord(e.target.value)} 
                    placeholder="הקלד לחיפוש..." 
                />
                <button type="submit" className="searchButton">{loading ? '...' : 'חפש'}</button>
            </form>
            <div className="resultsDiv">
                {results.map((r, i) => (
                    <div key={i} className="resultCard">
                        <p id="pasuk">{renderHighlightedText(r.text)}</p>
                        <div className="source-line">
                            (חומש {r.chumashName}, פרשת {r.parashaName}, פרק {r.perekName})
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default AdvancedSearch;