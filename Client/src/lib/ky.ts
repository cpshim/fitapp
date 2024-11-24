import ky from "ky";

const kyApi = ky.create({
  prefixUrl: "http://localhost:8080/api",
  credentials: "include",
});

export { kyApi as ky };
