#include <iostream>
#include <string>

using namespace std;

#define ull unsigned long long
string src_str, target_str;

ull my_hash(string src, int len){

    ull result = 0;

    for (int i = 0; i < len; i++)
        result += src[i] << i;

    return result;
}
ull my_hash_update(ull old_hash, string src, char old_char, char new_char, int m){
    old_hash = (old_hash - old_char) / 2;
    old_hash += + new_char << (m - 1);
    return old_hash;
}

int main(){

    string current_str;

    getline(cin, src_str);
    getline(cin, target_str);

    int n = src_str.length();
    int m = target_str.length();
    
    if (n < m)
        return 0;

    ull target_key = my_hash(target_str, m);
    ull current_key = my_hash(src_str, m);

    int start = 0;
    do {
        current_str = src_str.substr(start, m);
        if (current_key == target_key){
            if (current_str.compare(target_str) == 0)
                cout << start << endl;
        }
        if (start + m < n)
            current_key = my_hash_update(current_key, current_str, src_str[start], src_str[start + m], m);
        start++;
    } while (start <= n - m);

    current_str = src_str.substr(start, m);
    if (current_key == target_key){
        if (current_str.compare(target_str) == 0)
            cout << start << endl;
    }

    return 0;
}