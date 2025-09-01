export const UserRole = {
  Employer: "Employer",
  JobSeeker: "JobSeeker",
} as const;

export type UserRole = (typeof UserRole)[keyof typeof UserRole];
