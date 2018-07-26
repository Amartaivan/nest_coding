def factorial(k):
    if k == 1:
        return 1
    return k * factorial(k - 1)

if __name__ == "__main__":
    k = input()
    print factorial(k)