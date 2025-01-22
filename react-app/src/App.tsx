import "./App.css";
import BookPage from "./components/Books/BookPage";
import Footer from "./components/Footer/Footer";
import Header from "./components/Header/Header";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import MyBooksPage from "./components/MyBooksPage/MyBooksPage";

function App() {
  return (
    <BrowserRouter>
      <div className="app-wrapper">
        <Header />
        <div className="app-wrapper-content">
          <Routes>
            <Route path="/books" element={<BookPage />} />
            <Route path="/mybooks" element={<MyBooksPage />} />
          </Routes>
        </div>
        <Footer />
      </div>
    </BrowserRouter>
  );
}

export default App;
