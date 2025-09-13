import JobApplicationCard from "./JobApplicationCard";
import StatSection from "./StatSection";

export default function RecentJobApplications() {
  return (
    <>
      <StatSection
        title="Recent job applications"
        subTitle="Latest candidate applications"
        headerSection={
          <button className="text-sm text-blue-600 hover:text-blue-700 font-medium">
            View all
          </button>
        }
      >
        <div className="space-y-3">
          {Array.from({ length: 3 }).map(() => (
            <JobApplicationCard />
          ))}
        </div>
      </StatSection>
    </>
  );
}
