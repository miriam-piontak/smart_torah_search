import React, { useState, useEffect, useMemo } from 'react';
import toraService from "../../services/toraService";
import './Home.css';

const Home = () => {
    const [word, setWord] = useState('');
    const [exactMatch, setExactMatch] = useState(false);
    const [results, setResults] = useState([]);
    const [loading, setLoading] = useState(false);
    const [searchDone, setSearchDone] = useState(false);

    const [chumashes, setChumashes] = useState([]);
    const [parashaMap, setParashaMap] = useState({}); 
    const [allParashas, setAllParashas] = useState([]);
    const [dynamicPereks, setDynamicPereks] = useState([]);

    const [selectedChumash, setSelectedChumash] = useState('הכל');
    const [selectedParasha, setSelectedParasha] = useState('הכל');
    const [selectedPerek, setSelectedPerek] = useState('הכל');

    const defaultPereks = ['א','ב','ג','ד','ה','ו','ז','ח','ט','י','יא','יב','יג','יד','טו','טז','יז','יח','יט','כ','כא','כב','כג','כד','כה','כו','כז','כח','כט','ל','לא','לב','לג','לד','לה','לו','לז','לח','לט','מ','מא','מב','מג','מד','מה','מו','מז','מח','מט','נ'];

    useEffect(() => {
        const initData = async () => {
            try {
                const names = await toraService.getChumashesNames();
                setChumashes(names);
                let map = {};
                let flatList = [];
                for (let name of names) {
                    const data = await toraService.getParashaByChumash(name);
                    const cleanData = (data || []).filter(p => p && p.trim() !== "").map(p => p.trim());
                    map[name] = cleanData;
                    flatList = [...flatList, ...cleanData];
                }
                setParashaMap(map);
                setAllParashas([...new Set(flatList)]);
            } catch (err) { console.error("Error loading data:", err); }
        };
        initData();
    }, []);

    useEffect(() => {
        const updatePereks = async () => {
            try {
                if (selectedParasha !== 'הכל') {
                    let chumashToUse = selectedChumash;
                    if (chumashToUse === 'הכל') {
                        chumashToUse = Object.keys(parashaMap).find(c => parashaMap[c].includes(selectedParasha));
                    }
                    if (chumashToUse) {
                        const data = await toraService.getPereksByParasha(chumashToUse, selectedParasha);
                        setDynamicPereks(data || []);
                    }
                } else if (selectedChumash !== 'הכל') {
                    const data = await toraService.getPereksByChumash(selectedChumash);
                    setDynamicPereks(data || []);
                } else {
                    setDynamicPereks([]);
                }
            } catch (err) { setDynamicPereks([]); }
        };
        updatePereks();
        setSelectedPerek('הכל');
    }, [selectedParasha, selectedChumash, parashaMap]);

    const displayParashas = useMemo(() => {
        if (selectedChumash === 'הכל') return allParashas;
        return parashaMap[selectedChumash] || [];
    }, [selectedChumash, allParashas, parashaMap]);

    const displayPereks = dynamicPereks.length > 0 ? dynamicPereks : defaultPereks;

    const handleSearch = async (e) => {
        if (e) e.preventDefault();
        setLoading(true);
        setSearchDone(false);
        try {
            let data = [];
            let type = exactMatch ? "חיפוש מדויק" : "חיפוש רגיל";
            
            if (word.trim()) {
                data = exactMatch 
                    ? await toraService.getPsukimByWordExact(word) 
                    : await toraService.getPSUKIMbyWORD(word);
                
                if (selectedChumash !== 'הכל') data = data.filter(r => r.chumashName === selectedChumash);
                if (selectedParasha !== 'הכל') data = data.filter(r => r.parashaName === selectedParasha);
                if (selectedPerek !== 'הכל') data = data.filter(r => r.perekName === selectedPerek);
            } else if (selectedChumash !== 'הכל' || selectedParasha !== 'הכל' || selectedPerek !== 'הכל') {
                data = await toraService.filterByLocation(selectedChumash, selectedParasha, selectedPerek);
                type = "חיפוש לפי מיקום";
            }
            
            setResults(data || []);
            
            // שמירה להיסטוריה
            await toraService.createLineToHistory(type, word || "ללא מילה", selectedChumash, selectedParasha, selectedPerek);
            
        } catch (err) { 
            console.error("Search failed:", err);
            setResults([]); 
        } finally { 
            setLoading(false); 
            setSearchDone(true); 
        }
    };

    const highlightWord = (text, query) => {
        if (!query || !query.trim()) return text;
        const parts = text.split(new RegExp(`(${query})`, 'gi'));
        return (
            <span>
                {parts.map((part, i) => 
                    part.toLowerCase() === query.toLowerCase() ? <span key={i} className="color-letter">{part}</span> : part
                )}
            </span>
        );
    };

    return (
        <div className="mainDiv">
            <h1 className="main-title">חיפוש בתורה</h1>
            <form onSubmit={handleSearch} className="searchForm">
                <button type="submit" className="searchButton">{loading ? '...' : 'חפש בתורה'}</button>
                <input 
                    type="text" 
                    className="search-input" 
                    placeholder="הקלד מילה לחיפוש..." 
                    value={word}
                    onChange={(e) => setWord(e.target.value)}
                />
            </form>
            <div className="options-container">
                 <label className="checkbox-label">
                    חיפוש מדויק
                    <input type="checkbox" checked={exactMatch} onChange={(e)=>setExactMatch(e.target.checked)} />
                </label>
            </div>
            <div className="filters-wrapper">
                <div className="filters-grid">
                    <div className="filter-item">
                        <label>חומש:</label>
                        <select value={selectedChumash} onChange={(e) => { setSelectedChumash(e.target.value); setSelectedParasha('הכל'); }}>
                            <option value="הכל">כל החומשים</option>
                            {chumashes.map(c => <option key={c} value={c}>{c}</option>)}
                        </select>
                    </div>
                    <div className="filter-item">
                        <label>פרשה:</label>
                        <select value={selectedParasha} onChange={(e) => setSelectedParasha(e.target.value)}>
                            <option value="הכל">כל הפרשיות</option>
                            {displayParashas.map(p => <option key={p} value={p}>{p}</option>)}
                        </select>
                    </div>
                    <div className="filter-item">
                        <label>פרק:</label>
                        <select value={selectedPerek} onChange={(e) => setSelectedPerek(e.target.value)}>
                            <option value="הכל">כל הפרקים</option>
                            {displayPereks.map(p => <option key={p} value={p}>{p}</option>)}
                        </select>
                    </div>
                </div>
            </div>
            <div className="results-area">
                {results.map((r, i) => (
                    <div key={i} className="resultCard">
                        <p id="pasuk">{highlightWord(r.text, word)}</p>
                        <div className="source-line">
                            (חומש {r.chumashName}, פרשת {r.parashaName}, פרק {r.perekName}, פסוק {r.name})
                        </div>
                    </div>
                ))}
                {searchDone && results.length === 0 && (
                    <div className="no-results">לא נמצאו תוצאות התואמות לחיפוש</div>
                )}
            </div>
        </div>
    );
};

export default Home;