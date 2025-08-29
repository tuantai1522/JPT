import axios from "axios";

declare module "axios" {
  interface AxiosRequestConfig {
    _retry?: boolean;
  }
}

import { BASE_URL } from "../../features/shared/utils/apiPaths";

export const api = axios.create({
  baseURL: BASE_URL,
  withCredentials: true,
});
