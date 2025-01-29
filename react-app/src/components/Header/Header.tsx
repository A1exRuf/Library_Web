import s from "./Header.module.css";
import { NavLink } from "react-router-dom";

function Header() {
  return (
    <header className={s.header}>
      <div className={s.item}>
        <NavLink
          to="/books"
          className={(navData) => (navData.isActive ? s.active : "")}
        >
          Books
        </NavLink>
      </div>
      <div className={s.item}>
        <NavLink
          to="/mybooks"
          className={(navData) => (navData.isActive ? s.active : "")}
        >
          My books
        </NavLink>
      </div>
      <div className={s.signIn}>
        <NavLink
          to="/signin"
          className={(navData) => (navData.isActive ? s.active : "")}
        >
          Sign in
        </NavLink>
      </div>
    </header>
  );
}

export default Header;
