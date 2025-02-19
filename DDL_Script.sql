IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CalculoSeguroVeiculos')
BEGIN
    CREATE DATABASE CalculoSeguroVeiculos;
    PRINT 'Banco de dados CalculoSeguroVeiculos criado com sucesso!';
END

CREATE TABLE Segurado (
    SeguradoID INT IDENTITY(1,1) PRIMARY KEY,  -- Identificador único do segurado
    Nome NVARCHAR(255) NOT NULL,               -- Nome do segurado
    CPF CHAR(11) NOT NULL UNIQUE,              -- CPF do segurado (11 caracteres)
    Idade INT NOT NULL                         -- Idade do segurado
);

CREATE TABLE Veiculo (
    VeiculoID INT IDENTITY(1,1) PRIMARY KEY,    -- Identificador único do veículo
    Valor DECIMAL(18,2) NOT NULL,               -- Valor do veículo
    MarcaModelo NVARCHAR(255) NOT NULL          -- Marca/Modelo do veículo
);

CREATE TABLE Seguro (
    SeguroID INT IDENTITY(1,1) PRIMARY KEY,    -- Identificador único do seguro
    ValorSeguro DECIMAL(18,2) NOT NULL,        -- Valor do seguro
    TaxaRisco DECIMAL(5,2) NOT NULL,           -- Taxa de risco (%)
    PremioRisco DECIMAL(18,2) NOT NULL,        -- Prêmio de risco
    PremioPuro DECIMAL(18,2) NOT NULL,         -- Prêmio puro
    PremioComercial DECIMAL(18,2) NOT NULL,    -- Prêmio comercial
    SeguradoID INT NOT NULL,                   -- Chave estrangeira para a tabela Segurado
    VeiculoID INT NOT NULL,                    -- Chave estrangeira para a tabela Veiculo
    FOREIGN KEY (SeguradoID) REFERENCES Segurado(SeguradoID),
    FOREIGN KEY (VeiculoID) REFERENCES Veiculo(VeiculoID)
);

