USE DB_CONCESSIONARIA;

SELECT * FROM VENDA;

SELECT * FROM VENDEDOR;

SELECT * FROM Veiculo;

SELECT Vendedor.VendedorID AS "ID", Vendedor.Nome, (Vendedor.SalarioBase + (SUM(Veiculo.Valor)*0.01)) AS "Salário", MONTH(Venda.DataVenda) AS "Mês", YEAR(Venda.DataVenda) AS "Ano"
FROM Venda 
INNER JOIN Vendedor 
ON Vendedor.VendedorId = Venda.VendedorId
INNER JOIN Veiculo
ON Veiculo.VeiculoId = Venda.VeiculoId
GROUP BY Vendedor.VendedorId, Vendedor.Nome, Vendedor.SalarioBase, MONTH(Venda.DataVenda), YEAR(Venda.DataVenda);