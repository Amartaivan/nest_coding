#include <iostream>
#include <vector>

using namespace std;

class sportsman{
	public:
		int score;
		int id;
		
		sportsman(int s, int i) : score(s), id(i) {}
		
		void print(){
			cout << id + 1 << ' ';
		}
		
		bool operator> (sportsman operand){
			
			return score > operand.score;
		}
		bool operator< (sportsman operand){
			
			return score < operand.score;
		}
		bool operator== (sportsman operand){
			
			return score == operand.score;
		}
};

int main(){
	
	int n, tmpscore, i, j;
	cin >> n;
	
	vector<sportsman> A;
	for (i = 0; i < n; i++){
		
		cin >> tmpscore;
		sportsman tmp(tmpscore, i);
		
		A.push_back(tmp);
	}
	
	for (j = n - 1; j > 0; j--){
		
        i = 0;
        for (i = 0; i < j; i++)
            if (A[i] < A[i + 1] || (A[i] == A[i + 1] && A[i].id > A[i + 1].id))
                swap(A[i], A[i + 1]);	
    }
    
    for (auto a : A)
    	a.print();
	return 0;
}
