import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from "./components/NavBar/Navbar.jsx";
import AdvancedSearch from "./components/AdvancedSearch/AdvancedSearch.jsx";
import Home from "./components/Home/Home.jsx"; // ייבוא דף הבית
import Sgula from "./components/Sgula/Sgula.jsx"; // ייבוא דף הסגולה
import History from "./components/History/History.jsx"; // ייבוא דף ההיסטוריה
function App() {
  return (
    <Router>
      <div className="App">
        <Navbar /> 
        <Routes>
          {/* דף הבית עכשיו מציג את החיפוש והסינונים הרגילים */}
          <Route path="/" element={<Home />} />
          
          {/* חיפושים מתקדמים (סופי תיבות, גימטריה וכו') */}
          <Route path="/advanced" element={<AdvancedSearch />} />
          
          {/* דף הסגולה עם הפסוק לפי שם */}
          <Route path="/segula" element={<Sgula />} />
          
          {/* היסטוריה - בינתיים נשאיר זמני או שתרצי שנבנה גם אותו? */}
          <Route path="/history" element={<History />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;