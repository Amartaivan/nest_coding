def revnum(n):
    result = ""
    if n > 0:
        result += str(n % 10)
        return result + revnum(n / 10)
    else:
        return result

if __name__ == "__main__":
    n = input()
    print revnum(n)