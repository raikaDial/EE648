function [ Y ] = Parse_NGSpice( file )
%PARSE_NGSPICE Parses an NGSpice .txt file into a an array
%   There isn't really much more to say.

fid = fopen(file);

tline = 'empty';

while(~strcmp(tline,'Variables:')) % Get to the variable definitions
    tline = fgetl(fid);
end

i=0;
while(~strcmp(tline,'Values:')) % Count the number of variables
    i=i+1;
    tline = fgetl(fid);    
end

VarCount = i-1;

% Count the number of lines

i=0;
while(~isnumeric(fgetl(fid)))
    i=i+1;
end
    
TotalBlocks = i/(VarCount + 1);
TimeCount = 1;

Y = zeros(TotalBlocks,VarCount);

fid = fopen(file);

while(~strcmp(fgetl(fid),'Values:'))
end

for(i=1:TotalBlocks)
    for(i2=1:VarCount)
    tline = fgetl(fid);
        if(i2 == 1)
            segs = strsplit(tline);
            tline = segs{3};
        end
    Y(i,i2) = str2double(tline);
    end
    fgets(fid);
end

