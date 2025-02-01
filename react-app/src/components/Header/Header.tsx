import { useDispatch, useSelector } from "react-redux";
import s from "./Header.module.css";
import { NavLink } from "react-router-dom";
import { AppDispath, RootState } from "../../state/store";
import { logout } from "../../state/user/signInSlice";
import useUserRole from "../../hooks/useUserRole";

function Header() {
  const dispatch = useDispatch<AppDispath>();

  const isAuthenticated = useSelector(
    (state: RootState) => state.signIn.isAuthenticated
  );

  const userRole = useUserRole();

  const handleLogout = () => {
    dispatch(logout());
  };

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
      {userRole === "Admin" && (
        <div className={s.item}>
          <NavLink
            to="/addbook"
            className={(navData) => (navData.isActive ? s.active : "")}
          >
            Add book
          </NavLink>
        </div>
      )}

      {isAuthenticated ? (
        <div className={s.logout} onClick={handleLogout}>
          Logout
        </div>
      ) : (
        <div className={s.signIn}>
          <NavLink
            to="/signin"
            className={(navData) => (navData.isActive ? s.active : "")}
          >
            Sign in
          </NavLink>
        </div>
      )}
    </header>
  );
}

export default Header;
