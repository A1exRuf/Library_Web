import { NavLink, useNavigate } from "react-router-dom";
import s from "./SignInPage.module.css";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AppDispath, RootState } from "../../state/store";
import { login } from "../../state/user/signInSlice";

function SignInPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { loading, error, isAuthenticated } = useSelector(
    (state: RootState) => state.signIn
  );
  const dispatch = useDispatch<AppDispath>();
  const navigate = useNavigate();

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    dispatch(login({ email, password }));
  };

  if (isAuthenticated) {
    navigate("/books");
  }

  return (
    <div className={s.container}>
      {error && <p className={s.error}>{error}</p>}
      <h1>Sign in</h1>
      <form onSubmit={handleSubmit}>
        <h2 className={s.inputName}>Email:</h2>
        <input
          className={s.input}
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <h2 className={s.inputName}>Password:</h2>
        <input
          className={s.input}
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button className={s.submit} type="submit" disabled={loading}>
          {loading ? "Loading..." : "Sign in"}
        </button>
      </form>
      <p className={s.haveAccount}>
        Don't have an account?{" "}
        <NavLink
          to="/signup"
          className={(navData) => (navData.isActive ? s.active : "")}
        >
          create one
        </NavLink>
      </p>
    </div>
  );
}

export default SignInPage;
