import { useEffect, useLayoutEffect, useState } from "react";
import { AuthContext } from "../contexts/useAuth";
import { api } from "../../../lib/utils/api";
import { API_PATHS } from "../../shared/utils/apiPaths";
import type { RenewAccessTokenResponse } from "../../shared/types/users/renewToken.types";
import type { GetAccessTokenResponse } from "../../shared/types/users/getAccesToken.types";

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [token, setToken] = useState<string | null>();

  useEffect(() => {
    const fetchMe = async () => {
      try {
        const response = await api.get<GetAccessTokenResponse>(
          API_PATHS.USERS.GET_ACCESS_TOKEN
        );
        setToken(response?.data?.token);
      } catch {
        setToken(null);
      }
    };

    fetchMe();
  }, []);

  useLayoutEffect(() => {
    const authInterceptors = api.interceptors.request.use((config) => {
      config.headers.Authorization =
        !config._retry && token
          ? `Bearer ${token}`
          : config.headers.Authorization;

      return config;
    });

    return () => {
      api.interceptors.request.eject(authInterceptors);
    };
  }, [token]);

  useLayoutEffect(() => {
    const refreshInterceptors = api.interceptors.response.use(
      (response) => response,
      async (error) => {
        const originalRequest = error.config;

        if (error.response.status === 401) {
          try {
            const response = await api.get<RenewAccessTokenResponse>(
              API_PATHS.USERS.RENEW_TOKEN
            );
            setToken(response?.data?.token);

            originalRequest.headers.Authorization = `Bearer ${response?.data?.token}`;
            originalRequest.__retry = true;

            return api(originalRequest);
          } catch {
            setToken(null);
          }
        }

        return Promise.reject(error);
      }
    );

    return () => {
      api.interceptors.response.eject(refreshInterceptors);
    };
  }, []);

  return (
    <AuthContext.Provider value={{ token, setToken }}>
      {children}
    </AuthContext.Provider>
  );
};
