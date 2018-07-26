#include <iostream>
#include <vector>
#include <thread>

using namespace std;

//Not working with big arrays
vector<int> merge_sort(vector<int> A){

    int size = A.size();
    vector<int> result = A;

    if (size > 1){
        int midpoint = size / 2, finished_threads = 0;

        vector<int> left_half = vector<int>(A.begin(), A.begin() + midpoint), right_half = vector<int>(A.begin() + midpoint, A.end());
        auto sort_array = 
            [](vector<int>& left_half, int& finished_threads) { 
                left_half = merge_sort(left_half);
                return ++finished_threads;
            };
        thread  left(sort_array, ref(left_half), ref(finished_threads)),
                right(sort_array, ref(right_half), ref(finished_threads));
        left.join();
        right.join();
        while (finished_threads != 2) { /* wait */ };
        
        int i = 0, j = 0;

        while (i < left_half.size() && j < right_half.size()){
            if (left_half[i] > right_half[j]){
                result[i + j] = right_half[j];
                j++;
            }
            else {
                result[i + j] = left_half[i];
                i++;
            }
        }
        if (i == left_half.size()){
            while (j < right_half.size()){
                result[i + j] = right_half[j];
                j++;
            }
        }
        else {
            while (i < left_half.size()){
                result[i + j] = left_half[i];
                i++;
            }
        }
    }
    return result;
}

int main(){

    int n;
    cin >> n;
    vector<int> A(n);
    for (int i = 0; i < n; i++)
        cin >> A[i];
    
    A = merge_sort(A);

    for (int a : A)
        cout << a << ' ';
    cout << endl;
    return 0;
}