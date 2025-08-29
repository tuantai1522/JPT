import { useEffect, useLayoutEffect, useState } from "react";
import { AuthContext } from "../hooks/useAuth";
import { api } from "../../../lib/utils/api";
import { API_PATHS } from "../../shared/utils/apiPaths";
import type { GetCurrentUserResponse } from "../../shared/types/users/getCurrentUser";
import type { RefreshTokenResponse } from "../../shared/types/users/refreshToken";

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [token, setToken] = useState<string | undefined>(undefined);

  useEffect(() => {
    const fetchMe = async () => {
      try {
        const response = await api.get<GetCurrentUserResponse>(
          API_PATHS.USERS.GET_CURRENT_USER
        );
        setToken(response?.data?.token);
      } catch {
        setToken(undefined);
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
            const response = await api.get<RefreshTokenResponse>(
              API_PATHS.USERS.REFRESH_TOKEN
            );
            setToken(response?.data?.token);

            originalRequest.headers.Authorization = `Bearer ${token}`;
            originalRequest.__retry = true;

            return api(originalRequest);
          } catch {
            setToken(undefined);
          }
        }

        return Promise.reject(error);
      }
    );

    return () => {
      api.interceptors.response.eject(refreshInterceptors);
    };
  }, [token]);

  return (
    <AuthContext.Provider value={{ token, setToken }}>
      {children}
    </AuthContext.Provider>
  );
};
