import { useSelector } from "react-redux";
import { RootState } from "../state/store";
import { jwtDecode } from "jwt-decode";

function useUserRole(): string | null {
    const accessToken = useSelector((state: RootState) => state.signIn.accessToken);

    if (!accessToken) return null;

    const decodedToken = jwtDecode<Record<string, any>>(accessToken);
    const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    return decodedToken[roleClaim] || null;
}

export default useUserRole