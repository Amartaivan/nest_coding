#include <iostream>
#include <vector>

using namespace std;

class timestamp{
	
	public:
		int hours;
		int minutes;
		int seconds;
		int seconds_total;
		
		bool operator> (timestamp operand){
			
			return seconds_total > operand.seconds_total;
		}
		bool operator< (timestamp operand){
			
			return seconds_total < operand.seconds_total;
		}
		
		timestamp (int h, int m, int s) : hours(h), minutes(m), seconds(s){
			
			seconds_total = h * 3600 + m * 60 + s;
		}
		
		void print(){
			
			cout << hours << ' ' << minutes << ' ' << seconds << endl;
		}
};

int main(){
	
	int n, tmph, tmpm, tmps, i, j;
	cin >> n;
	
	vector<timestamp> A;
	for (i = 0; i < n; i++){
		
		cin >> tmph >> tmpm >> tmps;
		timestamp tmp(tmph, tmpm, tmps);
		
		A.push_back(tmp);
	}
	
	for (j = n - 1; j > 0; j--){
        i = 0;
        for (i = 0; i < j; i++)
            if (A[i] > A[i + 1])
                swap(A[i], A[i + 1]);
    }
    
    for (auto a : A)
    	a.print();
	return 0;
}
