function [ ] = MakePlots( file )
%MAKEPLOTS Makes plots out of shitty ngspice outputs
%   Detailed explanation goes here

Y = Parse_NGSpice(file);

hold off
n = 4.5;

plot(Y(:,1),Y(:,2) + 10 * n)
text(4e-9, 4 * n + 4, 'A')
hold on

plot(Y(:,1),Y(:,3) + 9 * n)
text(4e-9, 3 * n + 4, 'B')

plot(Y(:,1),Y(:,4) + 8 * n)
text(4e-9, 2 * n + 4, 'C')

plot(Y(:,1),Y(:,5) + 7 * n)
text(4e-9, 1 * n + 4, 'D')

plot(Y(:,1),Y(:,6) + 6 * n)
text(4e-9, 0 * n + 4, 'Y')

plot(Y(:,1),Y(:,7) + 5 * n)
text(4e-9, 0 * n + 4, 'Y')

plot(Y(:,1),Y(:,8) + 4 * n)
text(4e-9, 0 * n + 4, 'Y')

plot(Y(:,1),Y(:,9) + 3 * n)
text(4e-9, 0 * n + 4, 'Y')

plot(Y(:,1),Y(:,10) + 2 * n)
text(4e-9, 0 * n + 4, 'Y')

plot(Y(:,1),Y(:,11) + 1 * n)
text(4e-9, 0 * n + 4, 'Y')

plot(Y(:,1),Y(:,12) + 0 * n)
text(4e-9, 0 * n + 4, 'Y')

grid on

set(gca,'YTick',[]);
tick = 0:20:320;
set(gca,'XTick',tick*1e-9);
set(gca,'XTickLabel',[]);

axis([0 320e-9 -.5 11*n])







end

