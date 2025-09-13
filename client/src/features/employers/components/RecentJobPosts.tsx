import JobDashboardCard from "./JobDashboardCard";
import StatSection from "./StatSection";

export default function RecentJobPosts() {
  return (
    <>
      <StatSection
        title="Recent job posts"
        subTitle="Your latest job postings"
        headerSection={
          <button className="text-sm text-blue-600 hover:text-blue-700 font-medium">
            View all
          </button>
        }
      >
        <div className="space-y-3">
          {Array.from({ length: 3 }).map(() => (
            <JobDashboardCard />
          ))}
        </div>
      </StatSection>
    </>
  );
}
