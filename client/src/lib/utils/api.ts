import axios from "axios";
import { BASE_URL } from "../../features/shared/utils/apiPaths";

export const api = axios.create({
  baseURL: BASE_URL,
  withCredentials: true,
});
