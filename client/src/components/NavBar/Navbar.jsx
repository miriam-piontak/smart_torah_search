import React from 'react';
import { NavLink } from 'react-router-dom';
import './Navbar.css';

const Navbar = () => {
    return (
        <nav className="main-nav">
            <div className="nav-container">
                <div className="nav-logo">תורה-טק</div>
                <div className="nav-menu">
                    <NavLink to="/" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"}>דף הבית</NavLink>
                    <NavLink to="/segula" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"}>הפסוק לסגולה</NavLink>
                    <NavLink to="/advanced" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"}>חיפושים מתקדמים</NavLink>
                    <NavLink to="/history" className={({ isActive }) => isActive ? "nav-link active" : "nav-link"}>היסטוריה</NavLink>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;