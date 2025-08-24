import { api } from "../../../lib/utils/api";
import { API_PATHS } from "../../shared/utils/apiPaths";
import type { LoginRequest, LoginResponse } from "../type";

export const login = async (payload: LoginRequest): Promise<LoginResponse> => {
  const { data } = await api.post<LoginResponse>(
    API_PATHS.USERS.LOG_IN,
    payload
  );
  return data;
};
