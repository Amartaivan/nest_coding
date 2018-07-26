if __name__ == "__main__":
    a, n = map(int, raw_input().split())
    multiply = a
    result = 1
    for i in range(1, n + 1):
        result += multiply
        multiply *= a
    print result