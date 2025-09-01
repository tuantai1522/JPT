import { api } from "../../../../lib/utils/api";
import type { GetCurrentUserResponse } from "../../types/users/getCurrentUser.types";
import { API_PATHS } from "../../utils/apiPaths";

export const getCurrentUser = async (): Promise<GetCurrentUserResponse> => {
  const { data } = await api.get<GetCurrentUserResponse>(
    API_PATHS.USERS.GET_CURRENT_USER
  );
  return data;
};
