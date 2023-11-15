import { endpoint } from "./endpoint";

async function getJwt(username) {
  const url =
    endpoint +
    "token?" +
    new URLSearchParams({
      username: username,
    });

  await fetch(url)
    .then((response) => response.json())
    .then((data) => console.log(data))
    .catch((error) => console.log(error));
}

export { getJwt };
