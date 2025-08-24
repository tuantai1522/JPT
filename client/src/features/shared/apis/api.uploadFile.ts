import { api } from "../../../lib/utils/api";
import { API_PATHS } from "../../shared/utils/apiPaths";
import type { UploadFileResponse } from "../types/files/uploadFile";

export const postFile = async (file: File): Promise<UploadFileResponse> => {
  const formData = new FormData();
  formData.append("file", file);

  const { data } = await api.post<UploadFileResponse>(
    API_PATHS.FILES.UPLOAD,
    formData,
    { headers: { "Content-Type": "multipart/form-data" } }
  );
  return data;
};
