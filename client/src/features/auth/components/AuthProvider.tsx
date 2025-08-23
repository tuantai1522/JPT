import { useEffect, useState } from "react";
import { AuthContext } from "../hooks/useAuth";

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [token, setToken] = useState<string>("");

  useEffect(function () {
    // Call API to fetch current user and access token
    const fetchMe = () => {};

    fetchMe();
  }, []);

  return <AuthContext value={{ token, setToken }}>{children}</AuthContext>;
};
