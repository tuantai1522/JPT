import { z } from "zod";
import { UserRole } from "../../shared/constants/userRole";

export const registerFormSchema = z
  .object({
    firstName: z.string().max(64),
    middleName: z.string().max(64).optional(),
    lastName: z.string().max(64).optional(),
    email: z.email(),
    password: z
      .string()
      .min(8, "Password must be at least 8 characters")
      .max(128),
    confirmPassword: z
      .string()
      .min(8, "Password must be at least 8 characters")
      .max(128),
    avatarId: z.uuid().optional(),
    avatarPath: z.string().optional(),
    logoId: z.uuid().optional(),
    role: z.enum(UserRole),
    companyName: z.string().max(128),
  })
  .refine((data) => data.password === data.confirmPassword, {
    path: ["confirmPassword"], // Errors attaches to field "confirmPassword"
    message: "Passwords do not match",
  });

export type RegisterFormSchema = z.infer<typeof registerFormSchema>;
