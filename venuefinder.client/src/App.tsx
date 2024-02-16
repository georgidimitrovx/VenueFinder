import './App.css';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import HomePage from './pages/HomePage';
import SignInPage from './pages/SignInPage';
import SignUpPage from './pages/SignUpPage';
import CategoryPage from './pages/CategoryPage';
import VenuePage from './pages/VenuePage';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/signIn" element={<SignInPage />} />
                <Route path="/signUp" element={<SignUpPage />} />
                <Route path="/category/:categoryName/:categoryPageParam?" element={<CategoryPage />} />
                <Route path="/venue/:venueId" element={<VenuePage />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;