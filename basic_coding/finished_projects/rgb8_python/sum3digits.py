if __name__ == '__main__':
    x = int(raw_input())
    print (x % 10) + (x / 10 % 10) + (x / 100 % 10)