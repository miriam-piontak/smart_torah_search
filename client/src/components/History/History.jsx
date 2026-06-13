import React, { useState, useEffect } from 'react';
import toraService from "../../services/toraService";
import './History.css';

const History = () => {
    const [history, setHistory] = useState([]);
    const [loading, setLoading] = useState(true);

    // טעינת ההיסטוריה מהשרת
    useEffect(() => {
        const fetchHistory = async () => {
            try {
                const data = await toraService.getHistory();
                // הפיכת הסדר כדי שהחיפושים האחרונים יהיו למעלה
                setHistory(data ? data.reverse() : []);
            } catch (err) {
                console.error("לא ניתן לטעון היסטוריה:", err);
            } finally {
                setLoading(false);
            }
        };
        fetchHistory();
    }, []);

    if (loading) return <div className="loader">טוען היסטוריה...</div>;

    return (
        <div className="history-container">
            <h1 className="history-title">היסטוריית חיפושים</h1>
            
            {history.length > 0 ? (
                <div className="table-wrapper">
                    <table className="history-table">
                        <thead>
                            <tr>
                                <th>סוג חיפוש</th>
                                <th>מילת מפתח</th>
                                <th>חומש</th>
                                <th>פרשה</th>
                                <th>פרק</th>
                                <th>תאריך</th>
                            </tr>
                        </thead>
                        <tbody>
                            {history.map((item, index) => (
                                <tr key={index}>
                                    <td><span className={`type-badge ${item.type === 'חיפוש מדויק' ? 'exact' : 'regular'}`}>{item.type}</span></td>
                                    <td className="word-cell">{item.word || "—"}</td>
                                    <td>{item.chumash || "הכל"}</td>
                                    <td>{item.parasha || "הכל"}</td>
                                    <td>{item.perek || "הכל"}</td>
                                    <td className="date-cell">{item.date ? new Date(item.date).toLocaleDateString('he-IL') : "—"}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            ) : (
                <div className="no-history">
                    <p>עדיין אין חיפושים מתועדים במערכת.</p>
                </div>
            )}
        </div>
    );
};

export default History;