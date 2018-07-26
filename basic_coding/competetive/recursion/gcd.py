def gcd(a, b):
    if a > 0 and b > 0
        if a > b:
            return gcd(a % b, b)
        else:
            return gcd(a, b % a)
    return a + b

if __name__ == "__main__":
    a, b = map(int, raw_input().split())
    print gcd(a, b)