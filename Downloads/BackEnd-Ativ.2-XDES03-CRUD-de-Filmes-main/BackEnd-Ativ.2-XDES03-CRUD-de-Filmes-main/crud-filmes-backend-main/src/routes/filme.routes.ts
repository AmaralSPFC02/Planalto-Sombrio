import { Router } from "express";
import { FilmeController } from "../controller/filme.controller";

const router = Router();
const filmeController = new FilmeController();

router.post("/", filmeController.create.bind(filmeController));
router.delete("/:id", filmeController.delete.bind(filmeController));
router.get("/", filmeController.findAll.bind(filmeController));
router.get("/:id", filmeController.findById.bind(filmeController));
router.put("/:id", filmeController.update.bind(filmeController));

export { router as filmeRoutes };
