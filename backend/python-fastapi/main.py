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
