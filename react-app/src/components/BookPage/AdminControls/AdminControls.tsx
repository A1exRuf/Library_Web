import useUserRole from "../../../hooks/useUserRole";
import s from "./AdminControls.module.css";

function AdminControls() {
  const userRole = useUserRole();

  if (userRole !== "Admin") return null;

  return (
    <div className={s.adminControls}>
      <button className={s.edit}>Edit</button>
      <button className={s.remove}>Remove</button>
    </div>
  );
}

export default AdminControls;
