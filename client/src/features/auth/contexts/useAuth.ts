import { createContext, use, type Dispatch, type SetStateAction } from "react";

type AuthState = {
  token?: string | null;
  setToken: Dispatch<SetStateAction<string | null | undefined>>;
};

export const AuthContext = createContext<AuthState | undefined>(undefined);

export const useAuth = () => {
  const authContext = use(AuthContext);

  if (!authContext) {
    throw new Error("useAuth must be used within AuthContext provider");
  }

  return authContext;
};
