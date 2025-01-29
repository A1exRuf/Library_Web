import { useDispatch, useSelector } from "react-redux";
import { register } from "../../state/user/signUpSlice";
import s from "./SignUp.module.css";
import { AppDispath, RootState } from "../../state/store";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

function SignUpPage() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [role, setRole] = useState("");

  const { userId, loading, error } = useSelector(
    (state: RootState) => state.signUp
  );

  const dispatch = useDispatch<AppDispath>();
  const navigate = useNavigate();

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    dispatch(register({ name, email, password, confirmPassword, role }));
  };

  if (userId) {
    navigate("/signin");
  }

  return (
    <div className={s.container}>
      <h1>Sign up</h1>
      <form onSubmit={handleSubmit}>
        <h2 className={s.inputName}>Name:</h2>
        <input
          className={s.input}
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
          minLength={3}
          maxLength={24}
        />

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
          required
          minLength={6}
          maxLength={24}
        />

        <h2 className={s.inputName}>Confirm password:</h2>
        <input
          className={s.input}
          type="password"
          value={confirmPassword}
          onChange={(e) => {
            setConfirmPassword(e.target.value);
            e.target.setCustomValidity(
              e.target.value !== password ? "Passwords do not match." : ""
            );
          }}
          required
        />

        <label className={s.roleSelector} htmlFor="User">
          <input
            type="radio"
            id="User"
            name="Role"
            value="User"
            onChange={(e) => setRole(e.target.value)}
            required
          />
          User
        </label>
        <label className={s.roleSelector} htmlFor="Admin">
          <input
            type="radio"
            id="Admin"
            name="Role"
            value="Admin"
            onChange={(e) => setRole(e.target.value)}
            required
          />
          Admin
        </label>

        <button className={s.submit} type="submit" disabled={loading}>
          {loading ? "Loading..." : "Sign up"}
        </button>
        {error && <p className={s.error}>{error}</p>}
      </form>
    </div>
  );
}

export default SignUpPage;
