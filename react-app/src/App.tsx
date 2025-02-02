import "./App.css";
import Footer from "./components/Footer/Footer";
import Header from "./components/Header/Header";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import MyBooksPage from "./components/MyBooksPage/MyBooksPage";
import BooksPage from "./components/BooksPage/BooksPage";
import BookPage from "./components/BookPage/BookPage";
import SignInPage from "./components/SignInPage/SignInPage";
import SignUpPage from "./components/SignUpPage/SignUpPage";
import AddBookPage from "./components/AddBookPage/AddBookPage";
import EditBookPage from "./components/EditBookPage/EditBookPage";

function App() {
  return (
    <BrowserRouter>
      <div className="app-wrapper">
        <div className="header-content">
          <Header />
        </div>
        <div className="app-wrapper-content">
          <Routes>
            <Route path="/" element={<BooksPage />} />
            <Route path="/addbook" element={<AddBookPage />} />
            <Route path="/books" element={<BooksPage />} />
            <Route path="/books/:id" element={<BookPage />} />
            <Route path="/books/edit/:id" element={<EditBookPage />} />
            <Route path="/mybooks" element={<MyBooksPage />} />
            <Route path="/signin" element={<SignInPage />} />
            <Route path="/signup" element={<SignUpPage />} />
          </Routes>
        </div>
        <div className="footer-content">
          <Footer />
        </div>
      </div>
    </BrowserRouter>
  );
}

export default App;
