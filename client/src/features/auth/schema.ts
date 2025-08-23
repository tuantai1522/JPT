import { z } from "zod";
import { userRoleValues } from "../shared/schema";

export const logInFormSchema = z.object({
  email: z.email(),
  password: z
    .string()
    .min(8, "Password must be at least 8 characters")
    .max(128),
});

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
    logoId: z.uuid().optional(),
    role: z.enum(userRoleValues),
    companyName: z.string().max(128),
  })
  .refine((data) => data.password === data.confirmPassword, {
    path: ["confirmPassword"], // Errors attachs to field "confirmPassword"
    message: "Passwords do not match",
  });
