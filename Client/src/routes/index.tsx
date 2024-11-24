import { createFileRoute } from "@tanstack/react-router";

import { useAuth } from "@/hooks/useAuth";

export const Route = createFileRoute("/")({
  component: HomeComponent,
});

function HomeComponent() {
  const auth = useAuth();

  return (
    <>
      <h3>Welcome {auth.user?.email || "Guest"}!</h3>
    </>
  );
}
