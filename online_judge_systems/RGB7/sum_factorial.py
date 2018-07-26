if __name__ == "__main__":
    n = input()
    factorial_current = 1
    result = 0

    for i in range(1, n + 1):
        factorial_current *= i
        result += factorial_current
    print result