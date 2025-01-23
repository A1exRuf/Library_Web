import "./App.css";
import Footer from "./components/Footer/Footer";
import Header from "./components/Header/Header";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import MyBooksPage from "./components/MyBooksPage/MyBooksPage";
import BooksPage from "./components/BooksPage/BooksPage";
import BookPage from "./components/BookPage/BookPage";

function App() {
  return (
    <BrowserRouter>
      <div className="app-wrapper">
        <Header />
        <div className="app-wrapper-content">
          <Routes>
            <Route path="/books" element={<BooksPage />} />
            <Route path="/mybooks" element={<MyBooksPage />} />
            <Route path="/book" element={<BookPage />} />
          </Routes>
        </div>
        <Footer />
      </div>
    </BrowserRouter>
  );
}

export default App;
