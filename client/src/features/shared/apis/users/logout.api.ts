import { api } from "../../../../lib/utils/api";
import { API_PATHS } from "../../utils/apiPaths";

export const logOut = async (): Promise<boolean> => {
  const { data } = await api.get<boolean>(API_PATHS.USERS.LOG_OUT);
  return data;
};
