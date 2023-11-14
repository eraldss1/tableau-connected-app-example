import os
import sys

import uvicorn
from dotenv import load_dotenv
from fastapi import FastAPI

from generate_jwt import get_jwt

app = FastAPI()


@app.get("/")
def root():
    return {"message": "A Tableau JWT generator"}


@app.get("/token")
def get_token(username):
    try:
        token = get_jwt(username)
        response = {"token": token}
    except Exception as e:
        print(e)
        response = {"status": "error", "messages": str(e)}
    finally:
        return response


if __name__ == "__main__":
    load_dotenv()
    PORT = int(os.getenv("PORT", 3000))

    if len(sys.argv) > 1:
        if sys.argv[1] == "dev":
            uvicorn.run("main:app", port=PORT, log_level="debug", reload=True)
    else:
        uvicorn.run("main:app", port=PORT, log_level="info", reload=False)
