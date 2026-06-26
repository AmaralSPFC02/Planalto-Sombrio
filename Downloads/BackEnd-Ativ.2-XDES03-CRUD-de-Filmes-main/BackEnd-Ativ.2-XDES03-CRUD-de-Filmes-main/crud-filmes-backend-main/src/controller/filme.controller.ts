import { Request, Response } from "express";
import { FilmeService } from "../services/filme.service";

const filmeService = new FilmeService();

export class FilmeController {

  async create(req: Request, res: Response) {
    try {
      const dados = req.body;
      const filme = await filmeService.create(dados);
      res.status(201).json(filme);
    } catch (error: any) {
      res.status(400).json({ error: error.message });
    }
  }

  async delete(req: Request, res: Response) {
    try {
      const id = Number(req.params.id);
      await filmeService.delete(id);
      res.status(204).send();
    } catch (error: any) {
      res.status(404).json({ error: error.message });
    }
  }

  async update(req: Request, res: Response) {
    try {
      const id = Number(req.params.id);
      const dados = req.body;
      const filme = await filmeService.update(id, dados);
      res.status(200).json(filme);
    } catch (error: any) {
      const status = error.message.includes("não encontrado") ? 404 : 400;
      res.status(status).json({ error: error.message });
    }
  }

  async findAll(req: Request, res: Response) {
    try {
      const filmes = await filmeService.findAll();
      res.status(200).json(filmes);
    } catch (error: any) {
      res.status(500).json({ error: "Erro interno ao buscar filmes." });
    }
  }

  async findById(req: Request, res: Response) {
    try {
      const id = Number(req.params.id);
      const filme = await filmeService.findById(id);
      res.status(200).json(filme);
    } catch (error: any) {
      res.status(404).json({ error: error.message });
    }
  }
}
