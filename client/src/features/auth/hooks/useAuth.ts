import { createContext, use, type Dispatch, type SetStateAction } from "react";

type AuthState = {
  token: string | undefined;
  setToken: Dispatch<SetStateAction<string>>;
};

export const AuthContext = createContext<AuthState | undefined>(undefined);

export const useAuth = () => {
  const authContext = use(AuthContext);

  if (!authContext) {
    throw new Error("useAuth must be used within AuthContext provider");
  }

  return authContext;
};
