import { api } from "../../../lib/utils/api";
import { API_PATHS } from "../../shared/utils/apiPaths";
import type { RegisterRequest } from "../types/register";

export const register = async (payload: RegisterRequest): Promise<string> => {
  const { data } = await api.post<string>(API_PATHS.USERS.REGISTER, payload);
  return data;
};
