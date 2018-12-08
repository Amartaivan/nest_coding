def dectobin(n):
    result = ""
    if n > 0:
        result += str(n & 1)
        return dectobin(n >> 1) + result
    else:
        return result

if __name__ == "__main__":
    n = input()
    print dectobin(n)