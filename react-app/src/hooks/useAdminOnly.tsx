import { useNavigate } from "react-router-dom";
import useUserRole from "./useUserRole";
import { useEffect } from "react";

const useAdminOnly = () => {
  const userRole = useUserRole();
  const navigate = useNavigate();

  useEffect(() => {
    if (userRole !== "Admin") {
      navigate("/books");
    }
  }, [userRole, navigate]);
};

export default useAdminOnly;
