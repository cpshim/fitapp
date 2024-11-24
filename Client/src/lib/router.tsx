import type { QueryClient } from "@tanstack/react-query";
import { createRouter } from "@tanstack/react-router";

import type { AuthData } from "@/hooks/useAuth";
import { queryClient } from "@/lib/query";
import { routeTree } from "@/routeTree.gen";

type RouterContext = {
  auth: AuthData;
  queryClient: QueryClient;
};

const router = createRouter({
  routeTree,
  defaultPreload: "intent",
  context: {
    auth: null as unknown as AuthData,
    queryClient,
  },
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

export { router };
export type { RouterContext };
