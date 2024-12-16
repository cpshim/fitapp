import ky from "ky";

const kyApi = ky.create({
  // prefixUrl: "http://localhost:8080/api",
  prefixUrl: "http://localhost:5194/api",
  credentials: "include",
});

export { kyApi as ky };
