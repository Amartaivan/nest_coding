#include <iostream>
#include <string>

using namespace std;

#define ull unsigned long long
string src_str, target_str;

ull my_hash(string src, int len){

    ull result = 0;

    for (int i = 0; i < len; i++)
        result += src[i];

    return result;
}
ull inline my_hash_update(ull old_hash, char old_char, char new_char){
    return old_hash - old_char + new_char;
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
            current_key = my_hash_update(current_key, src_str[start], src_str[start + m]);
        start++;
    } while (start <= n - m);

    current_str = src_str.substr(start, m);
    if (current_key == target_key){
        if (current_str.compare(target_str) == 0)
            cout << start << endl;
    }

    return 0;
}