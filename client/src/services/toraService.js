import axios from 'axios';

// הכתובת ל-API (בלי המילה swagger, היא שייכת רק לממשק התיעוד)
const API_URL = 'https://localhost:7019/api/Tora';

const toraService = {
    getSofeiTevot: async (word) => {
        // שימוש ב-Backticks (``) כדי שהמשתנה word יוזרק לנתיב בצורה נכונה
        const response = await axios.get(`${API_URL}/sofei-tevot-by-word/${word}`);
        return response.data;
    },

    getRasheiTevot: async (word) => {
        // שימוש ב-Backticks (``) כדי שהמשתנה word יוזרק לנתיב בצורה נכונה
        const response = await axios.get(`${API_URL}/rashei-tevot-by-word/${word}`);
        return response.data;
    },

    getGimatria: async (number) => {
        // שימוש ב-Backticks (``) כדי שהמשתנה word יוזרק לנתיב בצורה נכונה
        const response = await axios.get(`${API_URL}/gimeria-by-number/${number}`);
        return response.data;
    },

        getHistory: async () => {
        // שימוש ב-Backticks (``) כדי שהמשתנה word יוזרק לנתיב בצורה נכונה
        const response = await axios.get(`${API_URL}/get-history`);
        return response.data;
    },
    getChumashesNames: async () => {
        const response = await axios.get(`${API_URL}/chumash-names`);
        return response.data;
    },
        getParashaByChumash: async (chumashName) => {
        const response = await axios.get(`${API_URL}/get-parashas-by-chumash/${chumashName}`);
        return response.data;
    },
    getParashNames: async () => {
        const response = await axios.get(`${API_URL}/parashas-names`);
        return response.data;
    },
    getPereksNames: async () => {
        const response = await axios.get(`${API_URL}/pereks-names`);
        return response.data;
    },
    getPereksByParasha: async (chumashName , parashaName ) => {
        const response = await axios.get(`${API_URL}/pereks-by-parasha/${chumashName}/${parashaName}`);
        return response.data;
    },
    getPereksByChumash: async (chumashName) => {
        const response = await axios.get(`${API_URL}/pereks-by-chumash/${chumashName}`);
        return response.data;
    },
// toraService.js - עדכון פונקציית ההיסטוריה
createLineToHistory: async (type, word, chumash, parasha, perek) => {
    try {
        // שליחת הנתונים כחלק מה-URL כפי שהגדרת, אך ללא המשתנה 'line' המיותר
        const response = await axios.post(`${API_URL}/line-to-history/${type}/${word}/${chumash}/${parasha}/${perek}`);
        return response.data;
    } catch (error) {
        console.error("Error saving to history:", error);
        // אנחנו לא רוצים שהשגיאה בהיסטוריה תתקע את כל האתר, לכן רק נדפיס אותה
        return null; 
    }
},
    getChumashByParasha: async (parashaName) => {
        const response = await axios.get(`${API_URL}/chumash-to-parasha/${parashaName}`);
        return response.data;
    },
    getAllPsukimByAll: async (chumashName ,parashaName ,perekName ) => {
        const response = await axios.get(`${API_URL}/all-psukim-by-all/${chumashName}/${parashaName}/${perekName}`);
        return response.data;
    },
    getAllPsukim: async () => {
        const response = await axios.get(`${API_URL}/all-psukim`);
        return response.data;
    },
    getPSUKIMbyWORD: async (word) => {
        const response = await axios.get(`${API_URL}/search-word/${word}`);
        return response.data;
    },
    getPsukimByWordExact: async (word) => {
        const response = await axios.get(`${API_URL}/exact-search-word/${word}`);
        return response.data;
    },
    filterByLocation: async (chumashName, parashaName, perekName) => {
        const response = await axios.get(`${API_URL}/filtered-by-location/${chumashName}/${parashaName}/${perekName}`);
        return response.data;
    },
    getPsukimForSgula: async (word) => {
        const response = await axios.get(`${API_URL}/pasuk-by-name-sgula/${word}`);
        return response.data;
    },

};

export default toraService;